<template>
    <v-container>
        <v-form @submit.prevent="updateDiaper" ref="form">
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
//import Error from '../Error.vue'

export default {
    components: {
        //Error
    },
    data() {
        return {
            diaper: {},
            error: false,
            timeRule: [
                v => !!v || 'Time is required'
            ]
        }
    },
    props: {
        diaperId: Number  
    },
    created() {
        sessionService.getDiaperById(this.$route.params.id, this.diaperId)
            .then(res => {
                if (res.status == 200) {
                    this.diaper = res.data;
                }
            })
            .catch(err => {
                if (err) {
                    console.error(err);
                    this.error = true;
                }
            })
    },
    methods: {
        updateDiaper() {
            if (this.$refs.form.validate()) {
               sessionService.updateDiaper(this.diaper)
                .then(res => {
                    if (res.status == 201) {
                        //this.$router.push({name: 'session', params: {id: this.diaper.sessionId}});
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