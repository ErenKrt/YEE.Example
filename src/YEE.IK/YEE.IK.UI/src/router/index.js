import { useUserStore } from '@/store/userStore';
import { createRouter, createWebHistory } from 'vue-router'


const routes=[
    {
        path: '/',
        name: 'Login',
        component: ()=> import('@/views/loginView.vue'),
    },
    {
        path: '/user/create',
        name: 'UserCreate',
        component: ()=> import('@/views/userCreateView.vue'),
        meta:{
            needLogin:true
        }
    },

];


const router = createRouter({
    history: createWebHistory(),
    routes,
    scrollBehavior(to, from, savedPosition) {
        return { top: 0 }
    },
})

router.beforeEach((to, from, next)=>{
    const userStore= useUserStore();

    if(to.meta?.needLogin==true && userStore.isLogged==false){
        return next({name: 'Login'});
    }

    return next();
});

export default router;