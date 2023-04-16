import { Card, Input, Button } from "antd";
import React, { useState } from "react";
import "./login.css";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";

function Login({ type }) {
  const [username, setUsername] = useState(undefined);
  const [password, setPassword] = useState(undefined);
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      const payload = new FormData();

      payload.append("Username", username);
      payload.append("Password", password);

      const res = await axios.post(
        "https://localhost:7237/api/organization/login",
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
        <br />
        <Button block type="primary" onClick={handleLogin}>
          Login
        </Button>
        <br />
        <div className="login__link">
          <Link to="/organization/register">Can't Have Account? SignUp</Link>
        </div>
      </Card>
    </div>
  );
}

export default Login;
