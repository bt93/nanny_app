import axios from 'axios'

export default {
    getAllChildren() {
        return axios.get('/children');
    }
}