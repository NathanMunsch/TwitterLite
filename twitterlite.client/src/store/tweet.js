import { createStore } from "vuex";

export default {
    namespaced: true,
    state: {
        tweets: [],
    },
    mutations: {
        setTweets(state, tweets) {
            state.tweets = tweets;
        },
        deleteTweet(state, tweetId) {
            state.tweets = state.tweets.filter(tweet => tweet.id !== tweetId);
        },
    },
    actions: {
        async getUserTweets({ commit }, userId) {
            try {
                const response = await fetch(`https://localhost:7078/tweet/get-user-tweet/${userId}`, {
                    method: 'GET',
                    credentials: 'include'
                });
                if(!response.ok) {
                    throw new Error('Réponse réseau non réussie');
                }
                const data = await response.json();
                commit('setTweets', data.tweets);
            } catch (error) {
                console.error("Erreur lors de la récupération des tweets:", error);
            }
        }
    },
};