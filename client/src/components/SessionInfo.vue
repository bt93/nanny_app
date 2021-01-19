<template>
  <v-row>
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
                :item-height="160"
                max-height="260"
                bench="1"
            >
                <template v-slot:default="{ item }">
                    <v-list-item :key="item.mealId" class="flex-column">
                        <h3>Time: {{ formatTime(item.time) }}</h3>
                        <h3>Type: {{ item.type }}</h3>
                        <h3>Notes: {{ (item.notes) ? item.notes : 'N/A' }}</h3>
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
                    </v-list-item> 
                </template>
                
            </v-virtual-scroll>
          </v-card>
      </v-col>
  </v-row>
</template>

<script>
import moment from 'moment'

export default {
    props: {
        session: Object
    },
    methods: {
        formatTime(time) {
            let date = new Date(time);

            return moment(date).format('LT');
        }
    }
}
</script>

<style>

</style>