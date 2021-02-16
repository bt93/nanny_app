<template>
  <div>
    <ul class="options">
        <li><v-btn :to="{name: 'endSession'}">End Session</v-btn></li>
        <li><v-btn @click="addNap = true">Add Nap</v-btn></li>
        <li><v-btn :to="{name: 'addMeal'}">Add Meal</v-btn></li>
        <li><v-btn :to="{name: 'addDiaper'}">Add Diaper</v-btn></li>
        <li><v-btn id="delete" :to="{name: 'deleteSession', params: {id: sessionId} }">Delete Session</v-btn></li>
        <v-overlay :value="addNap">
            <v-card class="pa-12" color="white">
                <v-btn @click="addNap = !addNap">Back</v-btn>
                <add-nap />  
            </v-card>     
        </v-overlay>
    </ul>
  </div>
</template>

<script>
import AddNap from '@/components/sessionforms/AddNap'
export default {
  components: { AddNap },
    props: {
        sessionId: Number
    },
    data() {
        return {
            addNap: false
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