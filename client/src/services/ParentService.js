import axios from 'axios'

export default {
    getParents() {
        return axios.get('/parents');
    },

    addParent(parent, childId) {
        return axios.post(`/parents/child/${childId}`, parent);
    }
}