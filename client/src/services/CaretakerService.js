import axios from 'axios'

export default {
    getCaretaker() {
        return axios.get('/caretakers/current');
    },

    updateCaretaker(user) {
        return axios.put('/caretakers', user);
    },

    updatePassword(user) {
        return axios.put('/caretakers/password', user)
    }
}