export default $axios => ({
    create(firstName, lastName, email, password){
        return $axios.post(`/users/create`, {
            firstName,
            lastName,
            email,
            password
        });
    }
});