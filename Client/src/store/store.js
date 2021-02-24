import Vuex from 'vuex'
import Vue from 'vue'

Vue.use(Vuex)

export default new Vuex.Store({

    state: {       
    },

    mutations: {                              
    },
    actions: {                        
    },

    getters: {        
        
        getTokenHeader: state => {            
            return {
                headers: {                    
                    "Content-Type": "application/json"
                }

            }
        },
        getTokenHeaderFormData: state => {            
            return {
                headers: {
                    
                    "Content-Type": "multipart/form-data"
                }

            }
        }

    }

})

