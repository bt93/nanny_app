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
            starTime: nap.starTime,
            endTime: nap.endTime,
            notes: nap.notes
        })
    },

    // Service from meals
    addMeal(meal, sessionId) {
        return axios.post(`/sessions/${sessionId}/meals`, {
            time: meal.time,
            type: meal.type,
            notes: meal.notes
        });
    },

    //Services from diapers
    addDiaper(diaper, sessionId) {
        return axios.post(`/sessions/${sessionId}/diapers`, {
            time: diaper.time,
            notes: diaper.notes
        });
    }
}