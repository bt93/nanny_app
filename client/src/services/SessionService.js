import axios from 'axios'

export default {
    getCurrentSessions() {
        return axios.get('/sessions');
    },

    getSessionById(sessionId) {
        return axios.get(`/sessions/${sessionId}`);
    },

    createSession(session) {
        return axios.post(`/sessions/child/${session.childId}`, {
            childId: session.childId,
            dropOff: session.dropOff,
            notes: session.notes
        });
    },

    updateCurrentSession(session, sessionId) {
        return axios.put(`/sessions/${sessionId}`, {
            sessionId: sessionId,
            dropOff: session.dropOff,
            notes: session.notes,
            diapers: session.diapers,
            meals: session.meals,
            naps: session.naps    
        });
    },

    endSession(session) {
        return axios.put(`/sessions/end/${session.sessionId}`, {
            sessionId: session.sessionId,
            dropOff: session.dropOff,
            pickUp: session.pickUp,
            notes: session.notes
        })
    },

    deleteSession(sessionId) {
        return axios.delete(`/sessions/${sessionId}`);
    },

    // Services for naps
    getNapById(sessionId, napId) {
        return axios.get(`/sessions/${sessionId}/naps/${napId}`);
    },

    addNap(nap, sessionId) {
        return axios.post(`/sessions/${sessionId}/naps`, {
            startTime: nap.startTime,
            endTime: nap.endTime,
            notes: nap.notes
        });
    },

    updateNap(nap) {
        return axios.put(`/sessions/${nap.sessionId}/naps/${nap.napId}`, {
            startTime: nap.startTime,
            endTime: nap.endTime,
            notes: nap.notes
        });
    },

    // Service from meals
    getMealById(sessionId, mealId) {
        return axios.get(`/sessions/${sessionId}/meals/${mealId}`);
    },

    addMeal(meal, sessionId) {
        return axios.post(`/sessions/${sessionId}/meals`, {
            time: meal.time,
            type: meal.type,
            notes: meal.notes
        });
    },

    updateMeal(meal) {
        return axios.put(`/sessions/${meal.sessionId}/meals/${meal.mealId}`, {
            time: meal.time,
            type: meal.type,
            notes: meal.notes
        })
    },

    //Services from diapers
    getDiaperById(sessionId, diaperId) {
        return axios.get(`/sessions/${sessionId}/diapers/${diaperId}`);
    },

    addDiaper(diaper, sessionId) {
        return axios.post(`/sessions/${sessionId}/diapers`, {
            time: diaper.time,
            notes: diaper.notes
        });
    },

    updateDiaper(diaper) {
        return axios.put(`/sessions/${diaper.sessionId}/diapers/${diaper.diaperId}`, diaper)
    }
}