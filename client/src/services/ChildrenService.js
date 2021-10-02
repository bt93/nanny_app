import axios from 'axios'

export default {
    getAllChildren() {
        return axios.get('/children');
    },

    getChildById(childId) {
        return axios.get(`/children/${childId}`);
    },

    getAllergyTypes() {
        return axios.get(`/allergy/types`);
    },

    getAllergiesByAllergyTypeId(allergyTypeId) {
        return axios.get(`/allergy/${allergyTypeId}`);
    },

    addChild(child) {
        return axios.post('/children', child);
    },

    addAllergyToChild(childId, allergyId) {
        return axios.post(`/children/${childId}/allergy/${allergyId}`);
    },

    updateChild(child) {
        return axios.put(`/children/${child.childId}`, child);
    },

    removeAllergyFromChild(childId, allergyId) {
        return axios.delete(`/children/${childId}/allergy/${allergyId}`);
    }
}