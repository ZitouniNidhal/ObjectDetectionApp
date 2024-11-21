from flask import Flask, request, jsonify
import torch
import cv2
import numpy as np

app = Flask(__name__)

# Charger un modèle YOLO pré-entraîné
model = torch.hub.load('ultralytics/yolov5', 'yolov5s')  # Modèle YOLO pré-entraîné

@app.route('/predict', methods=['POST'])
def predict():
    file = request.files['image']
    img = np.asarray(bytearray(file.read()), dtype=np.uint8)
    img = cv2.imdecode(img, cv2.IMREAD_COLOR)

    # Prédiction des objets dans l'image
    results = model(img)
    
    # Récupérer les résultats sous forme de dictionnaire
    predictions = results.pandas().xywh[0].to_dict(orient="records")
    return jsonify(predictions)

if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0', port=5000)
