import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { BrowserRouter } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Provider } from 'react-redux';
import store from "./redux/store";
import { persistStore } from "redux-persist";
import { PersistGate } from 'redux-persist/integration/react';

const root = ReactDOM.createRoot(document.getElementById('root'));
const persistor = persistStore(store);

root.render(
  <Provider store={store}>
    <React.StrictMode>
    <BrowserRouter>
    <PersistGate persistor={persistor}>
    <App />
    </PersistGate>
    </BrowserRouter>
  </React.StrictMode>
  </Provider>
);
