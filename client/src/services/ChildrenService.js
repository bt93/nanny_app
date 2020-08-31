import axios from 'axios'

export default {
    getAllChildren() {
        return axios.get('/children');
    },

    getChildById(childId) {
        return axios.get(`/children/${childId}`);
    },

    addChild(child) {
        return axios.post('/children', child);
    },

    updateChild(child) {
        return axios.put(`/children/${child.childId}`, child);
    }
}