import React, { Component } from "react";
import { Route, Routes } from "react-router-dom";
import { Layout } from "./components/Layout";
import "./custom.css";
import Login from "./components/Login";
import Dashboard from "./components/Organization/Dashboard";
import Register from "./components/Organization/Register";
import UserRegister from "./components/User/Register";
import UserDashboard from "./components/User/Dashboard";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Routes>
          <Route path="organization">
            <Route path="login" element={<Login type={"Org"} />} />
            <Route path="dashboard" element={<Dashboard />} />
            <Route path="register" element={<Register />} />
          </Route>
          <Route path=":iam">
            <Route path="login" element={<Login type={"user"} />} />
            <Route path="register" element={<UserRegister />} />
            <Route path="dashboard" element={<UserDashboard />} />
          </Route>
        </Routes>
      </Layout>
    );
  }
}
