<template>
    <v-container>
        <v-form @submit.prevent="updateMeal" ref="form">
            <v-row>
                <v-text-field 
                    type="datetime-local"
                    label="Time"
                    color="white"
                    background-color="primary"
                    v-model="meal.time"
                    :rules="timeRule"
                />
            </v-row>
            <v-row>
                <v-select
                label="Meal Type"
                color="white"
                background-color="primary"
                v-model="meal.type"
                :rules="typeRule"
                :items="types"
                item-text="name"
                item-value="name"
                 />
            </v-row>
            <v-row>
                <v-textarea 
                label="Notes"
                background-color="primary"
                color="white"
                v-model="meal.notes"
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
       // Error
    },
    data() {
        return {
            meal: {},
            error: false,
            timeRule: [
                v => !!v || 'Time is required'
            ],
            typeRule: [
                v => !!v || 'Meal Type is required'
            ],
            types: [
                { name: 'Breakfast' },
                { name: 'Lunch' },
                { name: 'Diner' },
                { name: 'Snack' },
                { name: 'Other' }
            ]
        }
    },
    props: {
        mealId: Number
    },
    created() {
        sessionService.getMealById(this.$route.params.id, this.mealId)
        .then(res => {
            if (res.status == 200) {
                this.meal = res.data;
            }
        })
        .catch(err => {
            if (err) {
                console.error(err);
                this.error = true;
            }
        });
    },
    methods: {
        updateMeal() {
            if (this.$refs.form.validate()) {
               sessionService.updateMeal(this.meal)
                .then(res => {
                    if (res.status == 201) {
                        //this.$router.push({name: 'session', params: {id: this.meal.sessionId}});
                        window.location.reload();
                    }
                })
                .catch(err => {
                    if (err) {
                        console.log(err);
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