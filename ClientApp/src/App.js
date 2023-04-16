import React, { Component } from "react";
import { Route, Routes } from "react-router-dom";
import AppRoutes from "./AppRoutes";
import { Layout } from "./components/Layout";
import "./custom.css";
import Login from "./components/Login";
import Dashboard from "./components/Organization/Dashboard";
import Register from "./components/Organization/Register";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Routes>
          <Route path="/organization">
            <Route
              path="/organization/login"
              element={<Login type={"Org"} />}
            />
            <Route path="/organization/dashboard" element={<Dashboard />} />
            <Route path="/organization/register" element={<Register />} />
          </Route>
        </Routes>
      </Layout>
    );
  }
}
