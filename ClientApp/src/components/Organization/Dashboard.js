import { Button, Card, Input, Modal, DatePicker, Alert } from "antd";
import React, { useEffect, useState } from "react";
import { Navbar, NavbarBrand } from "reactstrap";
import Appoinments from "./Appoinments";
import axios from "axios";
import Users from "./Users";

function Dashboard() {
  const [loggedInData, setLoggedInData] = useState(null);
  const [isEdit, setIsEdit] = useState(false);
  const [title, setTitle] = useState(undefined);
  const [description, setDescription] = useState(undefined);
  const [dateRange, setDateRange] = useState([]);
  useEffect(() => {
    const data = localStorage.getItem("db");

    setLoggedInData(JSON.parse(data));
  }, []);

  const changeHandller = (_, date) => {
    setDateRange(date);
  };

  const createHandller = async () => {
    try {
      const payload = new FormData();
      payload.append("Name", title);
      payload.append("Description", description);
      payload.append("StartRange", new Date(dateRange[0]).toUTCString());
      payload.append("EndRange", new Date(dateRange[1]).toUTCString());
      payload.append("Intreval", 10);

      const res = await axios.post(
        "https://localhost:7237/api/organization/appoinment",
        payload,
        {
          headers: {
            "Content-Type": "multipart/form-data",
            Authorization: `Token ${localStorage.getItem("coffee")}`,
          },
        }
      );
      if (res.data.status) {
        alert("Appoinment Created");
        window.location.reload();
      }
    } catch (err) {
      console.error(err);
      alert("Something Went Wrong Try Again");
    }
  };

  return (
    <div>
      <Navbar className="ng-white border-bottom box-shadow" container light>
        <NavbarBrand>{loggedInData?.organizationName}</NavbarBrand>
      </Navbar>
      <br />
      <Alert
        description={`Your Users IAM Link : https://localhost:44421/${loggedInData?.slug}/login`}
      />
      <br />
      <Button onClick={() => setIsEdit(true)}>Create New Appoinment</Button>
      <br />
      <br />
      <br />
      <Appoinments />
      <br />
      <h3>Users</h3>
      <br />
      <Users />
      <Modal
        title="Add Appoinment"
        open={isEdit}
        onCancel={() => setIsEdit(false)}
        okText={"Create Appoinment With Slots"}
        onOk={createHandller}
      >
        <Card>
          <Input
            type={"text"}
            placeholder="Appoinment title"
            style={{ marginBottom: "12px" }}
            value={title}
            onChange={(e) => setTitle(e.target.value)}
          />
          <Input.TextArea
            type={"text"}
            placeholder="Appoinment description"
            style={{ marginBottom: "12px" }}
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
          <DatePicker.RangePicker showTime onChange={changeHandller} />
        </Card>
      </Modal>
    </div>
  );
}

export default Dashboard;
