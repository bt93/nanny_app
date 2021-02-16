<template>
  <v-container>
      <v-form>
          
      </v-form>
  </v-container>
</template>

<script>
import sessionService from '@/services/SessionService'
//import Error from '@/components/Error'

export default {
  components: { 
      //Error
   },
    data() {
        return {
            nap: {
                notes: ''
            },
            error: false
        }
    },
    methods: {
        submitNap() {
            sessionService.addNap(this.nap, this.$route.params.id)
                .then(res => {
                    if (res.status === 201) {
                        this.$router.push({name: 'session', params: {id: this.$route.params.id}})
                    }
                })
                .catch(err => {
                    if (err) {
                        console.error(err);
                        this.error = true;
                    }
                })
        }
    }
}
</script>

<style>

</style>