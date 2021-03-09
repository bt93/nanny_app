<template>
    <v-container>
        <v-form @submit.prevent="addDiaper" ref="form">
            <v-row>
                <v-text-field 
                    type="datetime-local"
                    label="Time"
                    color="white"
                    background-color="primary"
                    v-model="diaper.time"
                    :rules="timeRule"
                />
            </v-row>
            <v-row>
                <v-textarea 
                    label="Notes"
                    background-color="primary"
                    color="white"
                    v-model="diaper.notes"
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
//import Error from '@/components/Error.vue'

export default {
  components: { 
      //Error
    },
    data() {
        return {
            diaper: {
                notes: ''
            },
            prevRoute: '',
            error: false,
            timeRule: [
                v => !!v || 'Time is required'
            ]
        }
    },
    // checks to see which route is coming from and changes SessionReturn link acordingly
    beforeRouteEnter(to, from, next) {
        next(vm => {
            vm.prevRoute = from.name;
        })
    },
    methods: {
        addDiaper() {
            if (this.$refs.form.validate()) {
                sessionService.addDiaper(this.diaper, this.$route.params.id)
                .then(res => {
                    if (res.status === 201) {
                        //this.$router.push({name: 'session', params: {id: this.$route.params.id}});
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