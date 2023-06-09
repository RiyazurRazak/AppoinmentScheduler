import React, { useEffect, useState } from "react";
import { Button, Collapse, Timeline } from "antd";
import axios from "axios";
import moment from "moment/moment";
function Appoinments() {
  const [appoinments, setAppoinments] = useState([]);
  const [slots, setSlots] = useState([]);

  const getAppoinments = async (token) => {
    try {
      const res = await axios.get(
        "https://localhost:7237/api/organization/appoinment",
        {
          headers: {
            Authorization: `Token ${token}`,
          },
        }
      );
      const data = await res.data;
      setAppoinments(data.data);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    const token = localStorage.getItem("coffee");
    getAppoinments(token);
  }, []);

  const collapseHandller = async (id) => {
    try {
      setSlots([]);
      const res = await axios.get(
        `https://localhost:7237/api/organization/slot?id=${id}`,
        {
          headers: {
            Authorization: `Token ${localStorage.getItem("coffee")}`,
          },
        }
      );
      const data = await res.data;
      const sanitizedData = data.data
        .sort((a, b) => new Date(a.slotTime) - new Date(b.slotTime))
        .map((slot, index) => {
          return {
            color: slot.isBooked ? "red" : "green",
            children: (
              <>
                <p>Slot Number: {index + 1}</p>
                <p>Slot Time: {moment(slot.slotTime).format("MMM DD HH:MM")}</p>
              </>
            ),
          };
        });

      setSlots(sanitizedData);
    } catch (err) {
      console.error(err);
      setSlots([]);
    }
  };

  const deleteAppoinmentHandller = async (id) => {
    try {
      const res = await axios.delete(
        `https://localhost:7237/api/organization/appoinment?id=${id}`,
        {
          headers: {
            Authorization: `Token ${localStorage.getItem("coffee")}`,
          },
        }
      );
      if (res.data.status) {
        alert("Appoinment Removed Successfully With Its Slots");
        getAppoinments(localStorage.getItem("coffee"));
      } else {
        throw Error("err");
      }
    } catch (err) {
      alert("Something went wrong!");
    }
  };

  return (
    <div>
      <h3>List Of Appoinments</h3>
      <br />
      <Collapse onChange={collapseHandller}>
        {appoinments.map((appoinment) => (
          <Collapse.Panel header={appoinment?.name} key={appoinment?.id}>
            <p>{appoinment?.description}</p>
            <br />
            <Button
              type="primary"
              danger
              onClick={() => deleteAppoinmentHandller(appoinment?.id)}
            >
              Delete Appoinment
            </Button>
            <br />
            <br />
            <br />
            <Timeline items={slots} />
          </Collapse.Panel>
        ))}
      </Collapse>
    </div>
  );
}

export default Appoinments;
