import axios from 'axios'

export default {
    getCaretaker() {
        return axios.get('/caretakers/current');
    }
}