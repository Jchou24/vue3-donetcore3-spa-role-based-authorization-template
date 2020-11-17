<template>
  <div id="nav">
    <router-link to="/">Home</router-link> |
    <router-link to="/about">About</router-link> |
    <router-link to="/testAPI">Test weather API</router-link> |
    <router-link to="/authentication">Authentication</router-link> |
    {{authenticationInfo}}
  </div>
  <router-view/>
  <!-- <PageIdle /> -->
  <PageIdle :idleSeconds="3" :userConfirmSeconds="5" />
</template>

<script lang="ts">
import { defineComponent, computed, watch, ref } from 'vue'
import { useStore } from 'vuex'
import PageIdle from '@/components/PageIdle.vue'
import { IsLogin } from '@/api/authentication.ts'

export default defineComponent({
  name: 'App',
  components:{
    PageIdle
  },
  setup(){
    const store = useStore()
    const unauthInfo = "You are not log in"
    // console.log( "store.state.authentication", store.state.authentication )
    // console.log( "store.state.authentication.claims.name", store.state.authentication.claims.name )

    const NavigationMessage = () => `Hi, ${store.state.authentication.claims.name}! You are now log in!`
    const authInfo = ref(NavigationMessage())
    watch( () => store.state.authentication.claims.name, (name: string) => {
      authInfo.value = NavigationMessage()
    })
    
    const authenticationInfo = computed(() =>{
      return store.state.authentication.isAuthenticated ? authInfo.value : unauthInfo
    })

    IsLogin(store)

    return {
      authenticationInfo
    }
  }

});
</script>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>
