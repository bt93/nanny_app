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
    }
}