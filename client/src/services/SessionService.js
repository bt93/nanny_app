import axios from 'axios'

export default {
    getCurrentSessions() {
        return axios.get('/sessions');
    },

    getSessionById(sessionId) {
        return axios.get(`/sessions/${sessionId}`);
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
    }
}