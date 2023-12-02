import axios from "axios";
import { inject } from "vue";
import { useUserStore } from "@/store/userStore";
import auth from "./auth";
import user from "./user";


export default {
    install: (app, options)=>{
        const apiClient= axios.create({
            ...options
        });

        const userStore= useUserStore();
        apiClient.interceptors.request.use(request=>{
            if(userStore.isLogged){
                request.headers.Authorization= `Bearer ${userStore.token.accessToken}`;
            }
            return request;
        });

        apiClient.interceptors.response.use(response=>{
            return response.data;
        });

        const repositories = {
            auth: auth(apiClient),
            user: user(apiClient)
        }

        app.provide('API', repositories);
    }
}

export const useAPI= ()=>{
    return inject('API');
}