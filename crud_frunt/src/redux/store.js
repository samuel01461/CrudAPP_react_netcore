import { combineReducers, configureStore } from "@reduxjs/toolkit";
import authReducer from "./reducers/authReducer";
import productsReducer from "./reducers/productsReducer";
import storage from "redux-persist/lib/storage";
import { persistReducer } from "redux-persist";

const persistConfig = {
    key: "root",
    version: 1,
    storage
}

const reducers = combineReducers({
    auth: authReducer,
    products: productsReducer
});

const persist_reducer = persistReducer(persistConfig, reducers);

const store = configureStore({
    reducer: persist_reducer,
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware({
          serializableCheck: false,
        }),
});

export default store;