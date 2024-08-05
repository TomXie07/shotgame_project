### Shooting Game Project Report

---

#### How do users interact with your executable file? How do they open and control the software (exe file) you wrote?

Users can open the game by launching the executable file (`*.exe`). Upon launching, users will see a graphical game window. The game provides the following controls:

- **Interface Controls**:
  - **Rendering Mode Selection**: Users can select different rendering modes such as Ambient, Lambert, or Phong using radio buttons.
  - **Material and Lighting Adjustment**: Users can adjust lighting intensity and color, as well as material properties, using sliders and color pickers.
  - **Score Display**: The game interface displays the current score.
  - **Skybox Toggle**: Users can control whether to display the skybox using a checkbox.

- **Keyboard and Mouse Controls**:
  - Users can hide the current mouse pointer by right-clicking to better operate the camera.
  - Users can display the current mouse pointer by left-clicking to better operate the UI interface controls.
  - Users can use the keyboard and mouse for in-game operations such as moving the view and shooting.

### Detailed Keyboard and Mouse Controls

Keyboard control is primarily for movement and shooting, while mouse control is for adjusting the view and making selections. These controls are handled in the `handleInput` function, ensuring users can interact with the game through intuitive actions.

#### 1. Keyboard Controls

**Functionality**:
- **View Control**: Adjust the camera's direction or position using key presses.
- **Shooting Control**: Detect if the user triggers a shooting event.

**Implementation**:

- **View Control**:
  - **Move Forward/Backward**: Use the `W` key to move the view forward and the `S` key to move the view backward.
  - **Move Left/Right**: Use the `A` key to move the view left and the `D` key to move the view right.

- **Shooting Control**:
  - **Shoot**: Use the `Space` key to trigger the shooting event. When the user presses the `Space` key, the program detects this event and executes the corresponding logic to create a bullet and add it to the scene.

#### 2. Mouse Controls

**Functionality**:
- **View Rotation**: Adjust the camera's direction by moving the mouse.
- **Selection and Interaction**: Select UI controls or perform other interactions by clicking the mouse.

---

#### How does the program code work? How do classes and functions cooperate, and what are their roles?

##### Main Classes and Key Functions Overview

1. **ShotGame Class**:
   - The primary game logic class, responsible for initializing, updating, and rendering the game scene.

2. **Key Functions**:
   - `init()`: Initializes the game window, loads resources, and sets the initial state.
   - `update(float deltaTime)`: Updates the game logic, including object positions and collision detection.
   - `renderFrame()`: Renders each frame, including setting view matrices, rendering objects, and handling the user interface.

##### Detailed Function Descriptions

1. **init()**:
   - Initializes the OpenGL context, loads shaders and textures, and creates an octree for spatial partitioning and collision detection.
   - Sets the initial rendering mode, material properties, and light attributes.

2. **update(float deltaTime)**:
   - Calls the octree's `update` method to update the positions of all objects.
   - Calls the octree's `checkCollisionsAndRemove` method to perform collision detection and remove collided objects.

3. **renderFrame()**:
   - Updates the time, sets the projection and view matrices, and displays the frame rate.
   - Clears the screen and enables depth testing.
   - Sets shader parameters based on the current rendering mode.
   - Iterates through all objects in the octree and renders them based on their type.
   - Displays the ImGui user interface, allowing users to adjust rendering modes and material properties.
   - Draws the skybox if enabled.

4. **`LoadObject` Function and Its Purpose**

- **Function**: Loads 3D models from a specified path and converts the model data into a format usable by OpenGL.
- **Workflow**:
  1. **Load the Model**: Use `objl::Loader` to load the model file.
  2. **Create OpenGL Objects**:
     - Create VAO, VBO, and EBO.
     - Bind and configure these objects, uploading the model data to the GPU.
  3. **Set Vertex Attributes**: Configure the layout of vertex data, including positions, normals, and texture coordinates.
  4. **Store the Mesh**: Store the processed mesh data in a `TriMesh` object.
  5. **Return the Mesh**: Return a vector containing all loaded meshes.
  
#### Detailed Description of the Octree Class

##### Data Structures

- **AABB (Axis-Aligned Bounding Box)**:
  - Represents an axis-aligned bounding box, including position information (x, y, z) and size (size).
  - `intersects(const AABB& other)`: Determines if two AABBs intersect.

- **OctObject**:
  - Represents an object within the octree, including bounding box (bounds), velocity vector (vx, vy, vz), type (type), direction (direction), ID (id), rotation angle (rotationAngle), and rotation axis (rotationAxis).
  - `update(float dt)`: Updates the object's position, moving it based on velocity and direction.

##### Class Members

- **Member Variables**:
  - `MAX_OBJECTS`: Maximum number of objects stored in each node.
  - `MAX_LEVELS`: Maximum depth of the octree.
  - `level`: The level of the current node.
  - `objects`: Objects stored in the current node.
  - `bounds`: Bounding box of the current node.
  - `nodes[8]`: Array of child node pointers.

##### Member Functions

- **Constructor `Octree(int pLevel, const AABB& pBounds)`**:
  - Initializes an octree node, setting the level and bounding box.

- **Destructor `~Octree()`**:
  - Cleans up the memory of child nodes.

- **`clear()`**:
  - Clears objects from the current node and all child nodes.

- **`split()`**:
  - Splits the current node into eight child nodes, each representing one-eighth of the current node's space.

- **`getIndex(const AABB& aabb) const`**:
  - Determines which child node the given bounding box belongs to.
  - Returns an integer from 0 to 7 representing one of the eight child nodes.

- **`insert(const OctObject& obj)`**:
  - Inserts an object into the octree.
  - If the number of objects in the current node exceeds the maximum limit and the maximum depth has not been reached, the current node is split and the objects are redistributed among the child nodes.

- **`retrieve(std::vector<OctObject>& returnObjects, const AABB& aabb) const`**:
  - Retrieves all objects intersecting with the given bounding box.
  - Adds these objects to `returnObjects`.

- **`update(float dt)`**:
  - Updates all objects in the octree, calling each object's `update` method.
  - Reinserts updated objects to account for their new positions.

- **`gatherObjects(std::vector<OctObject>& allObjects) const`**:
  - Collects all objects in the octree.
  - Adds these objects to `allObjects`.

- **`checkCollisionsAndRemove(std::vector<std::pair<OctObject, OctObject>>& collisionPairs)`**:
  - Checks for collisions among objects in the octree.
  - If collisions are found, adds the collision pairs to `collisionPairs` and removes the collided objects.

---

#### Features and Distinctive Aspects

1. **Spatial Partitioning and Collision Detection**:
   - Utilizes an octree data structure for spatial partitioning and accelerated collision detection, suitable for 3D scenes with numerous objects.

2. **Multiple Rendering Modes**:
   - Supports Ambient, Lambert, and Phong rendering modes, allowing users to dynamically switch and adjust material properties through the interface.

3. **Real-Time User Interface**:
   - Integrates the ImGui library to provide an intuitive real-time user interface, enabling users to adjust rendering settings and view game status easily.

4. **Rotating Objects and Dynamic Lighting**:
   - Supports rotating objects and dynamic lighting, enhancing the richness of visual effects.

##### Inspiration and Development Process

- **Inspiration**:
  - Inspired by 3D graphics courses and classic 3D game development examples.
  - Aims to create a small 3D shooting game featuring spatial partitioning, collision detection, and multiple rendering modes.

- **Development Process**:
  - Started with a simple rendering pipeline, gradually adding material and lighting models.
  - Integrated the octree for efficient spatial management and collision detection.
  - Finally, added user interface and control features, allowing users to adjust rendering parameters in real-time.

---

#### Using CMake Commands to Compile the Project

1. **Install Necessary Software**:
   - Install Visual Studio 2022 and CMake.

2. **Compilation Steps**:
   - Open a command line window and navigate to the project's root directory.
   - Use CMake to generate Visual Studio 2022 project files.

     ```bash
     cmake .. -G "Visual Studio 17 2022"
     ```

   - Open the generated solution file (`.sln`) and compile the project in Visual Studio.

---
