<script setup>
import { useAPI } from '../utils/api';
import { ref } from 'vue';
import { useUserStore } from '../store/userStore'
import { useRouter } from 'vue-router'

const API = useAPI();
const email = ref();
const password = ref();
const notification = ref();
const isLoading = ref();
const userStore= useUserStore();
const router= useRouter();

const submit = async() => {
    isLoading.value = true;
    var response= await API.auth.login(email.value, password.value);

    if(!response.succeeded){
        notification.value={ type: 'error', text: response.errors[0] };
    }

    userStore.setToken(response.result.accessToken, response.result.expireDate);
    router.push({
        name: 'UserCreate'
    })
}

</script>
<template>
    <div class="login-container">
        <h2>Giriş Yap</h2>

        <div v-if="notification" :class="[`notification`, notification.type]">{{ notification.text }}</div>

        <form class="login-form" @submit.prevent="submit">
            <div class="form-group">
                <label for="email">Email:</label>
                <input type="email" id="email" name="email" required v-model="email">
            </div>
            <div class="form-group">
                <label for="password">Şifre:</label>
                <input type="password" id="password" name="password" required v-model="password">
            </div>
            <div class="form-group">
                <input type="submit" value="Giriş Yap" :disabled="isLoading">
            </div>
        </form>
    </div>
</template>

<style scoped>
body {
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    background-color: #f5f5f5;
    margin: 0;
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100vh;
}

.login-container {
    background-color: #ffffff;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
    width: 300px;
    text-align: center;
}

.login-container h2 {
    color: #333;
}

.login-form {
    display: flex;
    flex-direction: column;
}

.form-group {
    margin-bottom: 20px;
}

.form-group label {
    font-size: 14px;
    margin-bottom: 5px;
    display: block;
    color: #555;
}

.form-group input {
    padding: 10px;
    font-size: 14px;
    border: 1px solid #ddd;
    border-radius: 4px;
    width: 100%;
    box-sizing: border-box;
}

.form-group input[type="submit"] {
    background-color: #007BFF;
    color: #fff;
    cursor: pointer;
    transition: background-color 0.3s;
}

.form-group input[type="submit"]:hover {
    background-color: #0056b3;
}

.form-group input[type="submit"]:disabled {
    background-color: gray;
}

.notification {
    margin-top: 10px;
    padding: 10px;
    border-radius: 4px;
}

.success {
    background-color: #28a745;
    color: #fff;
}

.error {
    background-color: #dc3545;
    color: #fff;
}
</style>