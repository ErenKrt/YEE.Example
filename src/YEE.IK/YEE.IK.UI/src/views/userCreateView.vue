<script setup>
import { ref } from 'vue';
import { useAPI } from '../utils/api';

const firstName= ref();
const lastName= ref();
const email= ref();
const password= ref();
const isLoading= ref();
const API= useAPI();
const submit = async() => {
    isLoading.value = true;
    var response= await API.user.create(firstName.value, lastName.value, email.value, password.value);

    if(!response.succeeded){
        alert(response.errors[0]);
    }
    isLoading.value = false;
    alert("Kullanıcı başarıyla eklendi !");
}

</script>
<template>
    <div class="registration-container">
        <h2>Kullanıcı Ekle</h2>
        <form class="registration-form" @submit.prevent="submit">
            <div class="form-group">
                <label for="firstName">Adı:</label>
                <input type="text" id="firstName" name="firstName" required v-model="firstName">
            </div>
            <div class="form-group">
                <label for="lastName">Soyadı:</label>
                <input type="text" id="lastName" name="lastName" required v-model="lastName">
            </div>
            <div class="form-group">
                <label for="email">Email Adresi:</label>
                <input type="email" id="email" name="email" required v-model="email">
            </div>
            <div class="form-group">
                <label for="password">Şifresi:</label>
                <input type="password" id="password" name="password" required v-model="password">
            </div>
            <div class="form-group">
                <input type="submit" value="Ekle" :disabled="isLoading">
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

.registration-container {
    background-color: #ffffff;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
    width: 300px;
    text-align: center;
}

.registration-container h2 {
    color: #333;
}

.registration-form {
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
    background-color: #28a745;
    color: #fff;
    cursor: pointer;
    transition: background-color 0.3s;
}

.form-group input[type="submit"]:hover {
    background-color: #218838;
}
</style>