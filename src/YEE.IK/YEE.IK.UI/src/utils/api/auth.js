export default $axios => ({
    login(email, password){
        return $axios.post(`/auth/login`, {
            email,
            password
        });
    },
    register(firstName, lastName, email, password){
        return $axios.post(`/auth/register`, {
            firstName,
            lastName,
            email,
            password
        });
    }
});