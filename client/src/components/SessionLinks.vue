<template>
  <div>
    <ul class="options">
        <li><v-btn :to="{name: 'endSession'}">End Session</v-btn></li>
        <li><v-btn @click="addNap = true">Add Nap</v-btn></li>
        <li><v-btn @click="addMeal = true">Add Meal</v-btn></li>
        <li><v-btn @click="addDiaper = true">Add Diaper</v-btn></li>
        <li><v-btn id="delete" :to="{name: 'deleteSession', params: {id: sessionId} }">Delete Session</v-btn></li>
        <v-overlay :value="addNap">
            <v-card class="pa-12" color="black">
                <v-row justify="center">
                  <v-btn @click="addNap = false">Back</v-btn>  
                </v-row>
                <v-row justify="center" class="mt-5">
                    <h3>Add Nap</h3>
                </v-row>
                <add-nap />  
            </v-card>     
        </v-overlay>
        <v-overlay :value="addMeal">
            <v-card class="pa-12" color="black">
                <v-row justify="center">
                  <v-btn @click="addMeal = false">Back</v-btn>  
                </v-row>
                <v-row justify="center" class="mt-5">
                    <h3>Add Meal</h3>
                </v-row>
                <add-meal />  
            </v-card>     
        </v-overlay>
        <v-overlay :value="addDiaper">
            <v-card class="pa-12" color="black">
                <v-row justify="center">
                  <v-btn @click="addDiaper = false">Back</v-btn>  
                </v-row>
                <v-row justify="center" class="mt-5">
                    <h3>Add Diaper</h3>
                </v-row>
                <add-diaper />  
            </v-card>     
        </v-overlay>
    </ul>
  </div>
</template>

<script>
import AddNap from '@/components/sessionforms/AddNap'
import AddMeal from '@/components/sessionforms/AddMeal'
import AddDiaper from '@/components/sessionforms/AddDiaper'

export default {
  components: { 
      AddNap,
      AddMeal,
      AddDiaper
    },
    props: {
        sessionId: Number
    },
    data() {
        return {
            addNap: false,
            addMeal: false,
            addDiaper: false
        }
    }
}
</script>

<style>
.options {
    list-style: none;
    margin: 20px;
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
}

.options > li {
    margin: 20px;
}

#delete {
    background-color: red;
}
</style>