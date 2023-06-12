from typing import Optional, List
from datetime import datetime
from sqlmodel import Field, SQLModel, Relationship



class CrossFinishLine(SQLModel, table=True):
    """Base table"""

    id: Optional[int] = Field(default=None, primary_key=True, unique=True)
    date_created: datetime = Field(nullable=False)
    vehicle_id: int = Field(foreign_key="vehicle.id", nullable=False, index=True)


class Vehicle(SQLModel, table=True):
    """Base table"""

    id: Optional[int] = Field(default=None, primary_key=True, unique=True)
    school_name: str = Field(nullable=False, index=True)
    number: str = Field(nullable=False, index=True, unique=True)
    team_name: str = Field(nullable=False, index=True)
    tag_uid: str = Field(nullable=False, index=True, unique=True)

    # Relationships
    crossings: Optional[List[CrossFinishLine]] = Relationship()

    @property
    def previous_crossing(self) -> Optional[CrossFinishLine]:
        if len(self.crossings) < 2: return
        crossing = self.crossings[-2]
        return crossing

    @property
    def current_crossing(self) -> Optional[CrossFinishLine]:
        if len(self.crossings) == 0: return
        crossing = self.crossings[-1]
        return crossing

    @property
    def previous_lap_time_seconds(self) -> Optional[float]:
        """Convenient helper to get lap time between newest
            previous crossings.

        Returns:
            Optional[float]: Returns total time between both in seconds.
        """
        return self.lap_time_seconds(self.crossings[-1])

    def lap_time_seconds(self, crossing: CrossFinishLine) -> Optional[float]:
        """Calculates total lap time between `crossing` and `previous_crossing`.

        Args:
            crossing (CrossFinishLine): Line crossing to act as end time.

        Returns:
            Optional[float]: Returns total time between both in seconds.
        """
        crossing_index = self.crossings.index(crossing)
        if crossing_index == 0:
            return None
        previous_crossing = self.crossings[crossing_index - 1]
        lap_time = crossing.date_created - previous_crossing.date_created
        return lap_time.total_seconds()