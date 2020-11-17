import axios from 'axios'
import { Store } from 'vuex'

axios.defaults.withCredentials = true

interface ILogin{
    email: string;
} 

function GetUserInfo(store: Store<any>){
    return axios.get(process.env.VUE_APP_SERVER_URL + 'api/account/getuserinfo')
        .then( response =>{
            store.commit("authentication/SetClaims", response.data.claims)
            // console.log(response.data)
        })
}

function IsLogin(store: Store<any>){
    // console.log("Run IsLogin API")
    return axios.get(process.env.VUE_APP_SERVER_URL + 'api/account/islogin')
        .then( response => {
            store.commit("authentication/SetIsAuthenticated", Boolean(response.data))
            if(store.state.authentication.isAuthenticated){
                GetUserInfo(store)
            }
        })
}

function Login(data: ILogin, store: any){
    // console.log("Run Login API")
    return axios.post(process.env.VUE_APP_SERVER_URL + 'api/account/login', data)
        .then(()=>{
            GetUserInfo(store).then( () => {
                store.commit("authentication/SetIsAuthenticated", true)
            })
        })
}

function Logout(store: any){
    // console.log("run log out")
    store.commit("authentication/Init")
    store.commit("pageIdle/Init")
    return axios.post(process.env.VUE_APP_SERVER_URL + 'api/account/logout')
}

function KeepAlive(){
    return axios.post(process.env.VUE_APP_SERVER_URL + 'api/account/keepalive')
}

export {
    ILogin,
    IsLogin,
    Login,
    Logout,
    GetUserInfo,
    KeepAlive,
}