import axios from 'axios'

export default {
    getCurrentSessions() {
        return axios.get('/sessions');
    },

    getAllSessions() {
        return axios.get('/sessions/all');
    },

    getSessionById(sessionId) {
        return axios.get(`/sessions/${sessionId}`);
    },

    createSession(session) {
        return axios.post(`/sessions/child/${session.childId}`, session)
    },

    updateCurrentSession(session, sessionId) {
        return axios.put(`/sessions/${sessionId}`, session);
    },

    endSession(session) {
        return axios.put(`/sessions/end/${session.sessionId}`, session)
    },

    deleteSession(sessionId) {
        return axios.delete(`/sessions/${sessionId}`);
    },

    // Services for naps
    getNapById(sessionId, napId) {
        return axios.get(`/sessions/${sessionId}/naps/${napId}`);
    },

    addNap(nap, sessionId) {
        return axios.post(`/sessions/${sessionId}/naps`, nap);
    },

    updateNap(nap) {
        return axios.put(`/sessions/${nap.sessionId}/naps/${nap.napId}`, nap);
    },

    // Service from meals
    getMealById(sessionId, mealId) {
        return axios.get(`/sessions/${sessionId}/meals/${mealId}`);
    },

    addMeal(meal, sessionId) {
        return axios.post(`/sessions/${sessionId}/meals`, meal);
    },

    updateMeal(meal) {
        return axios.put(`/sessions/${meal.sessionId}/meals/${meal.mealId}`, meal)
    },

    //Services from diapers
    getDiaperById(sessionId, diaperId) {
        return axios.get(`/sessions/${sessionId}/diapers/${diaperId}`);
    },

    addDiaper(diaper, sessionId) {
        return axios.post(`/sessions/${sessionId}/diapers`, diaper);
    },

    updateDiaper(diaper) {
        return axios.put(`/sessions/${diaper.sessionId}/diapers/${diaper.diaperId}`, diaper)
    }
}