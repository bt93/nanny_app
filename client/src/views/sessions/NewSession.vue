<template>
  <v-container>
      <error v-if="error"/>
      <v-container v-else>
        <v-row>
          <v-col cols="12">
            <v-card class="pa-12">
              <v-row justify="center">
                <h2>New Session</h2>
              </v-row>
              <v-form @submit.prevent="submit" ref="form">
                <v-row>
                  <v-select 
                    label="Child"
                    v-model="session.childId"
                    :rules="childRule"
                    :items="children"
                    item-text="firstName"
                    item-value="childId"
                  />
                </v-row>
                <v-row>
                  <v-text-field
                    label="Drop Off"
                    v-model="session.dropOff"
                    type="datetime-local"
                    :rules="dropOffRule"
                  />
                </v-row>
                <v-row>
                  <v-textarea 
                    label="Notes"
                    v-model="session.notes"
                  />
                </v-row>
                <v-row justify="center" class="pt-6">
                  <v-btn type="submit">Add new Session</v-btn>
                </v-row>
              </v-form>
            </v-card>
          </v-col>
        </v-row>
      </v-container>
      
  </v-container> 
</template>

<script>
import childrenService from '@/services/ChildrenService'
import sessionService from '@/services/SessionService'
import Error from '../../components/Error.vue'

export default {
  components :{ 
    Error
  },
  data() {
    return {
      session: {
        notes: ''
      },
      children: [],
      error: false,
      childRule: [
        v => !!v || 'Child is required'
      ],
      dropOffRule: [
        v => !!v || 'Drop Off time is required'
      ]
    }
  },
  methods: {
    submit() {
      if (this.$refs.form.validate()) {
        sessionService.createSession(this.session)
        .then(res => {
          if (res.status === 201) {
            this.$router.push('/dashboard');
          }
        })
        .catch(err => {
          console.error(err);
          this.error = true;
        })
      }
      
    }
  },
  created() {
    childrenService.getAllChildren()
      .then(res => {
        if (res.status === 200) {
          this.children = res.data
        }
      })
      .catch(err => {
        if (err) {
          this.error = true;
        }
      });
  }
}
</script>

<style>

</style>