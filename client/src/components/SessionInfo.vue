<template>
    <v-container>
      <v-row v-if="session == {}" justify="center">
          <v-progress-circular 
            indeterminate
            color="primary"
          /> 
      </v-row> 
      <v-row v-else-if="session !== {}">
      <v-col v-if="session.naps.length > 0">
          <v-card>
            <h2>Naps</h2>
            <v-virtual-scroll
                :items="session.naps"
                :item-height="160"
                max-height="260"
                bench="1"
            >
                <template v-slot:default="{ item }">
                    <v-list-item :key="item.napId" class="flex-column">
                        <h3>Start time: {{ formatTime(item.startTime) }}</h3>
                        <h3 v-if="item.endTime">End Time: {{ formatTime(item.endTime) }}</h3>
                        <h3>Notes: {{ (item.notes) ? item.notes : 'N/A' }}</h3>
                        <v-btn 
                            v-if="$route.name === 'updateSession'" 
                            @click="getNap(item.napId)"
                        >Update Nap</v-btn>
                    </v-list-item>
                </template>
            </v-virtual-scroll>  
          </v-card>
          
            </v-col>
            <v-col v-if="session.meals.length > 0">
                <v-card>
                    <h2>Meals</h2>
                    <v-virtual-scroll
                        :items="session.meals"
                        :item-height="180"
                        max-height="280"
                        bench="1"
                    >
                        <template v-slot:default="{ item }">
                            <v-list-item :key="item.mealId" class="flex-column">
                                <h3>Time: {{ formatTime(item.time) }}</h3>
                                <h3>Type: {{ item.type }}</h3>
                                <h3>Notes: {{ (item.notes) ? item.notes : 'N/A' }}</h3>
                                <v-btn 
                                    v-if="$route.name === 'updateSession'" 
                                    @click="getMeal(item.mealId)"
                                >Update Meal</v-btn>
                            </v-list-item> 
                        </template>
                    </v-virtual-scroll>
                    
                </v-card>
            </v-col>
            <v-col class="diapers" v-if="session.child.needsDiapers && session.diapers.length > 0">
                <v-card>
                    <h2>Diapers</h2>
                    <v-virtual-scroll
                        :items="session.diapers"
                        :item-height="160"
                        max-height="260"
                        bench="1"
                    >
                        <template v-slot:default="{ item }">
                            <v-list-item :key="item.diaperId" class="flex-column">
                                <h3>Time: {{ formatTime(item.time) }}</h3>
                                <h3>Notes: {{ (item.notes) ? item.notes : 'N/A' }}</h3>
                                <v-btn 
                                    v-if="$route.name === 'updateSession'" 
                                    @click="getDiaper(item.diaperId)"
                                >Update Diaper</v-btn>
                            </v-list-item> 
                        </template>
                        
                    </v-virtual-scroll>
                </v-card>
            </v-col>
        </v-row>
        <v-container>
            <v-overlay :value="updateNap">
                <v-card class="pa-12" color="black">
                    <v-row justify="center">
                    <v-btn @click="updateNap = false">Back</v-btn>  
                    </v-row>
                    <v-row justify="center" class="mt-5">
                        <h3>Update Nap</h3>
                    </v-row>
                    <update-nap :napId="napId"/>  
                </v-card>     
            </v-overlay>
            <v-overlay :value="updateMeal">
                <v-card class="pa-12" color="black">
                    <v-row justify="center">
                    <v-btn @click="updateMeal = false">Back</v-btn>  
                    </v-row>
                    <v-row justify="center" class="mt-5">
                        <h3>Update Meal</h3>
                    </v-row>
                    <update-meal :mealId="mealId"/>  
                </v-card>     
            </v-overlay>
            <v-overlay :value="updateDiaper">
                <v-card class="pa-12" color="black">
                    <v-row justify="center">
                    <v-btn @click="updateDiaper = false">Back</v-btn>  
                    </v-row>
                    <v-row justify="center" class="mt-5">
                        <h3>Update Diaper</h3>
                    </v-row>
                    <update-diaper :diaperId="diaperId"/>  
                </v-card>     
            </v-overlay>
        </v-container> 
    </v-container>
  
</template>

<script>
import moment from 'moment'
import UpdateNap from '@/components/sessionforms/UpdateNap'
import UpdateMeal from '@/components/sessionforms/UpdateMeal'
import UpdateDiaper from '@/components/sessionforms/UpdateDiaper'

export default {
    components: {
        UpdateNap,
        UpdateMeal,
        UpdateDiaper
    },
    data() {
        return  {
            updateNap: false,
            updateMeal: false,
            updateDiaper: false,
            napId: null,
            mealId: null,
            diaperId: null
        }
    },
    props: {
        session: Object
    },
    methods: {
        formatTime(time) {
            let date = new Date(time);

            return moment(date).format('LT');
        },
        getNap(id) {
            this.updateNap = true;
            this.napId = id;
        },
        getMeal(id) {
            this.updateMeal = true;
            this.mealId = id;
        },
        getDiaper(id) {
            this.updateDiaper = true;
            this.diaperId = id;
        }
    }
}
</script>

<style>

</style>