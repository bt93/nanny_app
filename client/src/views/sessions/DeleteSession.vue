<template>
  <div>
      <error v-if="error"/>
      <div v-else>
        <h2 @click.prevent="deleteSession" class="options delete">Delete</h2>
        <h2><router-link class="options cancel" :to="{name: 'updateSession', prams: {id: $route.params.id}}">Cancel</router-link></h2>
      </div>
  </div>
</template>

<script>
import sessionService from '@/services/SessionService'
import Error from '../../components/Error.vue';

export default {
    components: {
        Error
    },
    data() {
        return {
            error: false
        }
    },
    methods: {
        deleteSession() {
            if (confirm("Are you sure you want to delete this session? You will lose this forever.")) {
                sessionService.deleteSession(this.$route.params.id)
                    .then(res => {
                        if (res.status === 204) {
                            this.$router.push('/dashboard');
                        }
                    })
                    .catch(err => {
                        console.error(err);

                    })
            }
        }
    }
}
</script>

<style>
.options {
    height: 100px;
    margin: 20px;
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    padding: 5px;
    border-radius: 20px;
}

.cancel {
    color: black;
    border: rgb(58, 81, 102) 2px solid;
}

.cancel:hover {
    color: white;
    background-color: rgb(58, 81, 102);
    cursor: pointer;
}
.delete {
    color: red;
    border: red 2px solid;
}

.delete:hover {
    background-color: red;
    color: white;
    cursor: pointer;
}
</style>