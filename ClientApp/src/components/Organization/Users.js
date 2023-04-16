import { Table } from "antd";
import React, { useEffect, useState } from "react";
import axios from "axios";

const columns = [
  {
    title: "Name",
    dataIndex: "name",
    key: "name",
  },
  {
    title: "Email Address",
    dataIndex: "emailAddress",
    key: "emailAddress",
  },
  {
    title: "Contact",
    dataIndex: "contactNumber",
    key: "contactNumber",
  },
];

function Users() {
  const [data, setData] = useState([]);
  const getUsers = async (token) => {
    try {
      const res = await axios.get(
        "https://localhost:7237/api/organization/users",
        {
          headers: {
            Authorization: `Token ${token}`,
          },
        }
      );
      const data = await res.data;
      setData(data.data);
    } catch (err) {
      console.log(err);
    }
  };
  useEffect(() => {
    const token = localStorage.getItem("coffee");
    getUsers(token);
  });
  return (
    <div>
      <Table dataSource={data} columns={columns} />
    </div>
  );
}

export default Users;
