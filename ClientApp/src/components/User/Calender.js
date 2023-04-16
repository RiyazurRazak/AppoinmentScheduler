import React, { useEffect, useState } from "react";
import { Badge, Calendar } from "antd";
import axios from "axios";
import moment from "moment";

const getListData = (value) => {
  let listData;
  switch (value.date()) {
    case 8:
      listData = [
        {
          type: "warning",
          content: "This is warning event.",
        },
        {
          type: "success",
          content: "This is usual event.",
        },
      ];
      break;
    case 10:
      listData = [
        {
          type: "warning",
          content: "This is warning event.",
        },
        {
          type: "success",
          content: "This is usual event.",
        },
        {
          type: "error",
          content: "This is error event.",
        },
      ];
      break;
    case 15:
      listData = [
        {
          type: "warning",
          content: "This is warning event",
        },
        {
          type: "success",
          content: "This is very long usual event。。....",
        },
        {
          type: "error",
          content: "This is error event 1.",
        },
        {
          type: "error",
          content: "This is error event 2.",
        },
        {
          type: "error",
          content: "This is error event 3.",
        },
        {
          type: "error",
          content: "This is error event 4.",
        },
      ];
      break;
    default:
  }
  return listData || [];
};
const getMonthData = (value) => {
  if (value.month() === 8) {
    return 1394;
  }
};

function Calender() {
  const [mySlots, setMySlots] = useState([]);

  const getSlots = async (id) => {
    try {
      console.log(id);
      const res = await axios.get(
        `https://localhost:7237/api/user/myslots?id=${id}`
      );
      setMySlots(res.data.data);
    } catch (err) {
      console.error(err);
      setMySlots([]);
    }
  };
  useEffect(() => {
    const token = localStorage.getItem("tea");
    getSlots(token);
  }, []);

  const dateCellRender = (value) => {
    return (
      <ul className="events">
        {mySlots.map((item, index) => {
          if (
            value.format("DD-MM-YYYY") ===
            moment(item.slotTime).format("DD-MM-YYYY")
          )
            return (
              <li key={index} style={{ borderColor: "green" }}>
                <p style={{ fontSize: "12px" }}>{item.name}</p>
              </li>
            );
          else return <></>;
        })}
      </ul>
    );
  };
  const cellRender = (current, info) => {
    if (info.type === "date") return dateCellRender(current);
    return info.originNode;
  };

  return (
    <div>
      <Calendar cellRender={cellRender} mode="month" fullscreen={false} />
    </div>
  );
}

export default Calender;
