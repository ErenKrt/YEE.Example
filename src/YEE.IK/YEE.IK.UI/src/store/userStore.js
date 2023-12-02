import { defineStore } from 'pinia'

export const useUserStore= defineStore("user",{
    state: ()=>{
        return {
            token:{
                accessToken: localStorage.getItem("innovbyte.esa.token.accessToken") || null,
                expireDate: localStorage.getItem("innovbyte.esa.token.expireDate") || null,
            },
        }
    },
    getters:{
        isLogged(state){
            return state.token.accessToken!=null &&
            state.token.accessToken!="null" && new Date() < new Date(state.token.expireDate)
        }
    },
    actions: {
        setToken(token, expireDate){
            this.token.accessToken= token;
            this.token.expireDate= expireDate;
            localStorage.setItem("innovbyte.esa.token.accessToken", token)
            localStorage.setItem("innovbyte.esa.token.expireDate", expireDate);
        },
    }
});