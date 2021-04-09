import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import env from '../environments/environments';

export const EventBus = new Vue();
Vue.use(Vuex)

const state = {
    stock: {},
    history: {},
    recommendation: {},
    error: {},
    loading: false,
};

const mutations = {
    setResponse(state, response) {
        state.history = response.history;
        state.recommendation = response.recommendation;
        state.stock = response.stock;
    },
    removeCurrent(state){
        state.history = {};
        state.recommendation = {};
        state.stock = {};
    },
    setLoading(state, loading) {
        state.loading = loading;
    },
    setError(state, error) {
        state.error = error;
    }
};

const getters = {};

const actions = {
    async getStockDataFromApi({ commit }, ticker) {
        commit('setLoading', true);
        await axios.get(`${env.VUE_APP_API}/api/stock/${ticker}`)
            .then((rsp) => {
                commit('setResponse', rsp.data);
            })
            .catch(e => {
                commit('removeCurrent');
                commit('setError',e);
            })
        commit('setLoading', false);
    }
};

export default new Vuex.Store({
    state,
    getters,
    actions,
    mutations
})