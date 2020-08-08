import axios from 'axios'

export default {
    getCurrentSessions() {
        return axios.get('/sessions');
    }
}