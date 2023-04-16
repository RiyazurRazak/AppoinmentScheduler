import React from "react";
import { useParams } from "react-router-dom";
import { Navbar, NavbarBrand } from "reactstrap";
import Appoinments from "./Appoinment";
import Calender from "./Calender";

function Dashboard() {
  const params = useParams();
  return (
    <div>
      <Navbar className="ng-white border-bottom box-shadow" container light>
        <NavbarBrand>{params.iam}</NavbarBrand>
      </Navbar>
      <br />
      <br />
      <h3>Today Schedule</h3>
      <br />
      <Calender />
      <br />
      <Appoinments />
      <br />
    </div>
  );
}

export default Dashboard;
