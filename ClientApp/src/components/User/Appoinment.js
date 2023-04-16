import React, { useEffect, useState } from "react";
import { Button, Collapse, Timeline } from "antd";
import axios from "axios";
import moment from "moment/moment";
import { useParams } from "react-router-dom";
function Appoinments() {
  const [appoinments, setAppoinments] = useState([]);
  const [slots, setSlots] = useState([]);
  const params = useParams();

  const getAppoinments = async () => {
    try {
      const res = await axios.get(
        `https://localhost:7237/api/user/appoinment?orgSlug=${params.iam}`
      );
      const data = await res.data;
      setAppoinments(data.data);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    getAppoinments();
  }, []);

  const cancelSlotHandller = async (id) => {
    try {
      const res = await axios.delete(
        `https://localhost:7237/api/user/slot?id=${id}`
      );
      if (res.data.status) {
        alert("Slot canceled For You");
        window.location.reload();
      }
    } catch (err) {
      alert("Something Went Wrong");
    }
  };

  const bookSlotHandller = async (id) => {
    try {
      const userId = localStorage.getItem("tea");
      const payload = new FormData();
      payload.append("Id", id);
      payload.append("BookedBy", userId);
      const res = await axios.put(
        "https://localhost:7237/api/user/slot",
        payload
      );
      if (res.data.status) {
        alert("Slot Confirmed For You");
        window.location.reload();
      }
    } catch (err) {
      alert("Something Went Wrong");
    }
  };
  const collapseHandller = async (id) => {
    try {
      setSlots([]);
      const res = await axios.get(
        `https://localhost:7237/api/user/slot?id=${id}`
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
                <Button
                  disabled={slot.isBooked}
                  onClick={() => bookSlotHandller(slot.id)}
                >
                  {slot.isBooked ? "Booked" : "Book Slot"}
                </Button>
                {slot.bookedBy === localStorage.getItem("tea") && (
                  <Button
                    danger
                    style={{ marginLeft: "8px" }}
                    onClick={() => cancelSlotHandller(slot.id)}
                  >
                    Cancel Appoinment
                  </Button>
                )}
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

  return (
    <div>
      <h4>List Of Appoinments Available</h4>
      <br />
      <Collapse onChange={collapseHandller}>
        {appoinments.map((appoinment) => (
          <Collapse.Panel header={appoinment?.name} key={appoinment?.id}>
            <p>{appoinment?.description}</p>
            <Timeline items={slots} />
          </Collapse.Panel>
        ))}
      </Collapse>
    </div>
  );
}

export default Appoinments;
