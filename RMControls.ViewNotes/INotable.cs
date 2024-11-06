using System;
using System.Collections.Generic;
using System.Text;

namespace RMControls.ViewNotes {
    public interface INotable {
        string getPitchName();
        int getForceDown();
        int getForceUp();
        float getStartTime();
        float getEndTime();

        void setPitchName(string pitchName);
        void setForceDown(int forceDown);
        void setForceUp(int forceUp);
        void setStartTime(float startTime);
        void setEndTime(float endTime);
    }
}
