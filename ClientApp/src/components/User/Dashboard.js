import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { Navbar, NavbarBrand } from "reactstrap";
import Appoinments from "./Appoinment";
import Calender from "./Calender";

function Dashboard() {
  const params = useParams();

  const [loggedInData, setLoggedInData] = useState(null);
  useEffect(() => {
    const data = localStorage.getItem("db");

    setLoggedInData(JSON.parse(data));
  }, []);

  return (
    <div>
      <Navbar className="ng-white border-bottom box-shadow" container light>
        <NavbarBrand>{params.iam}</NavbarBrand>
      </Navbar>
      <br />
      <h3>Howdy {loggedInData?.name}</h3>
      <h4>This Month Schedule For You</h4>
      <Calender />
      <br />
      <Appoinments />
      <br />
    </div>
  );
}

export default Dashboard;
