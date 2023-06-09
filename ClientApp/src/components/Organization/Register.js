import { Card, Input, Button } from "antd";
import React, { useState } from "react";
import "../login.css";
import axios from "axios";
import { useNavigate } from "react-router-dom";

function Register() {
  const [name, setName] = useState(undefined);
  const [email, setEmail] = useState(undefined);
  const [username, setUsername] = useState(undefined);
  const [password, setPassword] = useState(undefined);
  const [contact, setContact] = useState(undefined);
  const [address, setAddress] = useState(undefined);
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      const payload = new FormData();

      payload.append("OrganizationName", name);
      payload.append("EmailAddress", email);
      payload.append("Username", username);
      payload.append("Password", password);
      payload.append("ContactNumber", contact);
      payload.append("Address", address);

      const res = await axios.post(
        "https://localhost:7237/api/organization/register",
        payload,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );
      localStorage.setItem("coffee", res.data?.data?.token);
      localStorage.setItem("db", JSON.stringify(res.data?.data));
      navigate("/organization/dashboard");
    } catch (err) {
      console.log(err);
      alert("Something went wrong try again");
    }
  };
  return (
    <div className="login__root">
      <Card title="Organization Login" bordered={false} style={{ width: 350 }}>
        <Input
          type="text"
          placeholder="Organization Name"
          style={{ marginBottom: "18px" }}
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <Input
          type="email"
          placeholder="Organization email"
          style={{ marginBottom: "18px" }}
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <Input
          type="text"
          placeholder="username"
          style={{ marginBottom: "18px" }}
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <br />
        <Input
          type="password"
          placeholder="******"
          style={{ marginBottom: "18px" }}
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <Input
          type="text"
          placeholder="contact number"
          style={{ marginBottom: "18px" }}
          value={contact}
          onChange={(e) => setContact(e.target.value)}
        />
        <Input.TextArea
          type="text"
          placeholder="address"
          style={{ marginBottom: "18px" }}
          value={address}
          onChange={(e) => setAddress(e.target.value)}
        />
        <br />
        <Button block type="primary" onClick={handleLogin}>
          Register
        </Button>
      </Card>
    </div>
  );
}

export default Register;
