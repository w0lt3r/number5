import React, { Component } from 'react';
import {Route, Routes} from "react-router";
import './custom.css'
import {BrowserRouter} from "react-router-dom";
import PermissionManger from "./components/PermissionManager";
const App = () => {
    return <>
        <BrowserRouter>
            <Routes>
                {/*<Route*/}
                {/*    path="/"*/}
                {/*    element={ <Navigate to="/serviceexperts" /> }*/}
                {/*/>*/}
                <Route path="/" element={<PermissionManger />} />
            </Routes>
        </BrowserRouter>
    </>;
};

export default App;
