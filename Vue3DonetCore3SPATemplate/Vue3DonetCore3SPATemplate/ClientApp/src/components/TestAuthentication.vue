<template>
  <div class="">
    Email: <input v-model="email" :disabled="isAuthenticated">

    <button @click="login" :disabled="isAuthenticated" >
      login
    </button>

    <button @click="logout" :disabled="!isAuthenticated">
      logout
    </button>

    <div class="warning">
      <p v-for="(err, idx) in errorMessages" :key="idx">
        {{err}}
      </p>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, ref } from 'vue';
import { useStore } from 'vuex'
import { ILogin, Login, Logout } from '@/api/authentication.ts'

export default defineComponent({
  name: 'TestAuthentication',
  setup(props, { emit }){
    const store = useStore()
    const isAuthenticated = computed(() => store.state.authentication.isAuthenticated)
    const email = computed({
      get(){
        return store.state.authentication.claims.emailaddress
      },
      set(value){
        store.commit("authentication/SetEmail", value)
      }
    })
    const errorMessages = ref([])

    const logindata = computed(() => { 
      return {email: email.value}
    }) 

    function login(){
      Login(logindata.value as ILogin, store).then(() => {
        errorMessages.value = []
      }).catch(error=>{
        if (error.response.status == 400) {
          errorMessages.value = error.response.data["Email"]
        }
        console.log(error.response)
      })

      emit('login-success')
    }

    function logout(){
      Logout(store)
    }

    return {
      isAuthenticated,
      email,
      errorMessages,

      login,
      logout
    }
  }

});
</script>

<style lang="scss" scoped>
  .warning{
    color: orangered;
  }
</style>