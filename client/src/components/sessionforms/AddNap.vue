<template>
  <v-container>
      <v-form @submit.prevent="submitNap" ref="form">
          <v-row>
              <v-text-field 
            type="datetime-local"
            label="Start Time"
            color="white"
            background-color="primary"
            v-model="nap.startTime"
            :rules="timeRule"
          />
          </v-row>
          <v-row>
              <v-text-field 
            type="datetime-local"
            label="End Time"
            color="white"
            background-color="primary"
            v-model="nap.endTime"
            :rules="timeRule"
          />
          </v-row>
          <v-row>
              <v-textarea 
                label="Notes"
                background-color="primary"
                color="white"
                v-model="nap.notes"
              />
          </v-row>
          <v-row justify="center">
              <v-btn type="submit">Submit</v-btn>
          </v-row>
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
            error: false,
            timeRule: [
                v => !!v || 'Time is required'
            ]
        }
    },
    methods: {
        submitNap() {
            if (this.$refs.form.validate()) {
              sessionService.addNap(this.nap, this.$route.params.id)
                .then(res => {
                    if (res.status === 201) {
                        //this.$router.push({name: 'session', params: {id: this.$route.params.id}})
                        //this.$emit('close-overlay', false)
                        window.location.reload();
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
}
</script>

<style>

</style>