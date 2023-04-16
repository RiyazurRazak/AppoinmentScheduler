import React, { useEffect, useState } from "react";
import { Button, Collapse, Timeline } from "antd";
import axios from "axios";
import moment from "moment/moment";
import { useParams } from "react-router-dom";
function Appoinments() {
  const [appoinments, setAppoinments] = useState([]);
  const [slots, setSlots] = useState([]);
  const params = useParams();

  console.log(slots);
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
      <h3>List Of Appoinments Available</h3>
      <br />
      <Collapse onChange={collapseHandller}>
        {appoinments.map((appoinment, index) => (
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
