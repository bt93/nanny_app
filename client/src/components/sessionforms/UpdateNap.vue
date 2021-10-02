<template>
    <v-container>
      <v-form @submit.prevent="updateNap" ref="form">
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
//import Error from '../Error.vue'

export default {
    components: {
        //Error
    },
    data() {
        return {
            endTime: '',
            nap: {},
            error: false,
            timeRule: [
                v => !!v || 'Time is required'
            ]
        }
    },
    props: {
        napId: Number
    },
    created() {
        sessionService.getNapById(this.$route.params.id, this.napId)
            .then(res => {
                if (res.status == 200) {
                    this.nap = res.data;
                }
            })
            .catch(err => {
                console.error(err);
                this.error = true;
            })
    },
    methods: {
        updateNap() {
            if (this.$refs.form.validate()) {
              sessionService.updateNap(this.nap)
            .then(res => {
                if (res.status == 201) {
                    this.$router.push({name: 'session', params: { id: this.nap.sessionId }})
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