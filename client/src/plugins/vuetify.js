import Vue from 'vue';
import Vuetify from 'vuetify/lib/framework';

Vue.use(Vuetify);

export default new Vuetify({
    theme: {
        themes: {
          light: {
            primary: '#48dfb9',
            secondary: '#3a5166',
            background: '#DCDCDC'
          },
        },
      },
    
});
