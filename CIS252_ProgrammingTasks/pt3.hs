-- NAME: Joseph Busacca
-- EMAIL: Jebusacc@syr.edu

-- PROGRAMMING TASK 3
-- SOURCES: 
-- used hoogle.haskell.org to find isInfixOf for redact function 
import Robots

import Data.List (isInfixOf)


---------------------- 1 -------------------------------
-- PURPOSE
-- such that move (x,y) rbt has the effect of moving rbt to the location that is x units east and y units north of its current location.

-- DEFINITION
move :: (Integer, Integer) -> Robot -> Robot
move (x, y) (Drone (a, b) item log) = Drone (a+x, b+y) item log
move (x, y) (Rover (a, b) items log) = Rover (a+x, b+y) items log

-- TESTS
-- move (2, 3) sampleDrone    -- tests for positive integers and tests for drones
-- move (-1, -3) sampleRover1 -- takes for negative integers and tests for rovers
-- move (0, 0) sampleRover2   -- takes for zeros as input and tests rover2
---------------------- 2 -------------------------------
-- PURPOSE
-- such that reset rbt has the effect of erasing the log of rbt and removing any items held by rbt.

-- DEFINITION
reset :: Robot -> Robot
reset (Drone loc _ _) = Drone loc Nothing []
reset (Rover loc _ _) = Rover loc [] []

-- TESTS
-- reset sampleDrone  -- tests for drone
-- reset sampleRover1 -- tests for rover1
-- reset sampleRover2 -- tests for rover2
---------------------- 3 -------------------------------
-- PURPOSE
-- such that addEntry str rbt returns the robot state obtained by adding str to the front of rbt’s log.

-- DEFINITION
addEntry :: String -> Robot -> Robot
addEntry str (Drone loc item lst) = Drone loc item (str:lst)
addEntry str (Rover loc items lst) = Rover loc items ((str, loc):lst)

-- TESTS
-- addEntry "Test" sampleDrone  -- tests for drone
-- addEntry "Test" sampleRover1 -- tests for rover1
-- addEntry "Test" sampleRover2 -- tests for rover2
---------------------- 4 -------------------------------
-- PURPOSE
-- such that redact str rbt removes all occurrences of the string str from its log.

-- DEFINITION
redact :: String -> Robot -> Robot
redact str (Drone loc item lst) = Drone loc item (filter (/= str) lst)
redact str (Rover loc items lst) = Rover loc items (filter (\(s, _) -> not (isInfixOf str s)) lst)

-- TESTS
-- redact "wake" sampleDrone -- tests for drone
-- redact "game" sampleRover1 -- tests for rover1 and if str is not in robot
-- redact "Pickup Book" sampleRover2 -- tests for rover2
---------------------- 5 -------------------------------
-- PURPOSE
-- such that pickup thing rbt returns the robot state obtained when rbt picks up thing:
-- • When rbt is a drone, thing becomes the single item held by rbt.
-- • When rbt is a rover, thing gets added to the collection of items held by rbt.
-- An entry is added to the log to indicate that thing was picked up.

-- DEFINITION
pickup :: Item -> Robot -> Robot
pickup item (Drone loc _ lst) = Drone loc (Just item) (("Pickup " ++ show item) : lst)
pickup item (Rover loc items lst) = Rover loc (item:items) (("Pickup " ++ show item, loc):lst)

-- TESTS
-- pickup Book sampleDrone -- tests drone
-- pickup Device sampleRover1 -- tests rover1 and a new item
-- pickup Book sampleRover2 -- tests rover2. With test cases provided in instructions, all items are tested
---------------------- 6 -------------------------------
-- PURPOSE
-- such that dropAll thing rbt returns the robot state obtained when rbt drops all copies of thing that it may be holding. An entry is added to the robot’s log

-- DEFINITION
dropAll :: Item -> Robot -> Robot
dropAll thing (Drone loc item log)
  | item == Just thing = Drone loc Nothing (("DropAll " ++ show thing) : log)
  | otherwise = Drone loc item (("DropAll " ++ show thing) : log)
dropAll thing (Rover loc items log) = Rover loc (filter (/= thing) items) (("DropAll " ++ show thing, loc) : log)


-- TESTS
-- dropAll game sampleDrone -- tests for an error, should be Game
-- dropAll Toy sampleRover1 -- tests rover1 with Toy
-- dropAll Book sampleRover2 -- tests rover2 with book
---------------------- 7 -------------------------------
-- PURPOSE
-- such that perform i rbt returns the resulting state when rbt performs the instruction i. Note: This function should simply be a six-equation function that calls your previously defined functions

-- DEFINITION
perform :: Instr -> Robot -> Robot
perform (Move (x, y)) rbt = move (x, y) rbt
perform Reset rbt = reset rbt
perform (Log str) rbt = addEntry str rbt
perform (Redact str) rbt = redact str rbt
perform (Pickup item) rbt = pickup item rbt
perform (DropAll item) rbt = dropAll item rbt

-- TESTS
-- perform (Move (2,3)) sampleDrone
-- perform Reset sampleRover1           
-- perform (Pickup Device) sampleRover2 -- tests three different functions, same tests ran in their function to ensure perform is working correctly