<template>
  <div class="allSessions">
      <img src="@/images/loading.gif" alt="Loading" v-if="isLoading">
      <error v-else-if="error"/>
      <session-container v-else v-for="session in sessions" :key="session.sessionId" :session="session"/>
  </div>
</template>

<script>
import sessionService from '@/services/SessionService'
import Error from '../../components/Error.vue'
import SessionContainer from '../../components/SessionContainer.vue'

export default {
    components: {
        Error,
        SessionContainer
    },
    data() {
        return {
            sessions: [],
            error: false,
            isLoading: true
        }
    },
    created() {
        sessionService.getAllSessions()
            .then(res => {
                if (res.status == 200) {
                    this.sessions = res.data;
                    this.isLoading = false;
                }
            })
            .catch(err => {
                if (err) {
                    console.error(err);
                    this.isLoading = false;
                    this.error = true;
                }
            })
    }
}
</script>

<style>

</style>